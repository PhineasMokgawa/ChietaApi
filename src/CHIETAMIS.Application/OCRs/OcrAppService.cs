using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using CHIETAMIS.Organisations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.IO;
using Tesseract;
using System.Drawing.Imaging;
using System.Text;
using System.Diagnostics;
using System.Collections;
using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.PageSegmenter;
using UglyToad.PdfPig.DocumentLayoutAnalysis.ReadingOrderDetector;
using UglyToad.PdfPig.DocumentLayoutAnalysis.WordExtractor;
using System.Linq;
using Amazon.Textract;
using Amazon.Textract.Model;
using PdfSharp.Pdf.Advanced;

namespace CHIETAMIS.OCRs
{
    public class OcrAppService : CHIETAMISAppServiceBase
    {
        private readonly IRepository<Organisation> _organisationRepository;

        public OcrAppService(IRepository<Organisation> organisationRepository)
        {
            _organisationRepository = organisationRepository;
        }

        [HttpPost]
        public async Task<string> ExtractText(IFormFile file)
        {
            var outtext = "";
            string pageText = "";
            string tessdataPath = System.IO.Path.Combine(AppContext.BaseDirectory, "tessdata");
            string filePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), file.FileName);
            string outPath = System.IO.Path.GetTempPath() + "\\" + (Guid.NewGuid()).ToString();

            using (var stream = System.IO.File.Create(filePath))
            {

                await file.CopyToAsync(stream);
            }

            try
            {
                outtext = await ExtractTextFromPDF(filePath);
            }
            catch (Exception e)
            {
                var err = e;
                try
                {
                    outtext = await ExtractTextract(filePath);
                }
                catch (Exception e2)
                {
                    var err2 = e2;
                }
            }
            if (outtext == "")
            {
                try
                {
                    outtext = await ExtractTextract(filePath);
                }
                catch (Exception e)
                {
                    var err = e;
                }
            }

            return outtext;
        }

        [HttpPost]
        public async Task<string> ExtractTextFromImage(string filePath)
        {
            string pageText = "";
            string tessdataPath = System.IO.Path.Combine(AppContext.BaseDirectory, "tessdata");
            //string filePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), file.FileName);
            //string outPath = System.IO.Path.GetTempPath() + "\\" + (Guid.NewGuid()).ToString();

            //using (var stream = System.IO.File.Create(filePath))
            //{

            //    await file.CopyToAsync(stream);
            //}

            try
            {
                using (var engine = new TesseractEngine(tessdataPath, "eng", EngineMode.Default))
                {
                    // Load the image
                    using (var img = Pix.LoadFromFile(filePath))
                    {
                        using (var page = engine.Process(img))
                        {
                            var text = page.GetText();
                            Console.WriteLine("Accuracy: {0:P}", page.GetMeanConfidence());
                            return text;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                return null;
            }
        }


            [HttpPost]
        public async Task<string> ExtractTextFromPDF(string filePath)
        {
            //List<string> pdfContent = new List<string>();
            string pageText = "";
            string tessDataPath = System.IO.Path.Combine(AppContext.BaseDirectory, "tessdata");
            //string filePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), file.FileName);
            //string outPath = System.IO.Path.GetTempPath() + "\\" + (Guid.NewGuid()).ToString();

            //using (var stream = System.IO.File.Create(filePath))
            //{

            //    await file.CopyToAsync(stream);
            //}

            if (System.IO.File.Exists(filePath))
            {
                var sb = new StringBuilder();

                using (var document = PdfDocument.Open(filePath))
                {
                    foreach (var page in document.GetPages())
                    {
                        //int formerRotate = page.Rotation.Value;
                        //if (formerRotate != null)
                        //    page.Rotation = page.Rotation.Value + 90;
                        //else
                        //    page.Put(page.ROTATE, new PdfNumber(item.Degree));

                        // 0. Preprocessing
                        var letters = page.Letters; // no preprocessing

                        // 1. Extract words
                        var wordExtractor = NearestNeighbourWordExtractor.Instance;
                        var wordExtractorOptions = new NearestNeighbourWordExtractor.NearestNeighbourWordExtractorOptions()
                        {
                            Filter = (pivot, candidate) =>
                            {
                                // check if white space (default implementation of 'Filter')
                                if (string.IsNullOrWhiteSpace(candidate.Value))
                                {
                                    // pivot and candidate letters cannot belong to the same word 
                                    // if candidate letter is null or white space.
                                    // ('FilterPivot' already checks if the pivot is null or white space by default)
                                    return false;
                                }

                                // check for height difference
                                var maxHeight = Math.Max(pivot.PointSize, candidate.PointSize);
                                var minHeight = Math.Min(pivot.PointSize, candidate.PointSize);
                                if (minHeight != 0 && maxHeight / minHeight > 2.0)
                                {
                                    // pivot and candidate letters cannot belong to the same word 
                                    // if one letter is more than twice the size of the other.
                                    return false;
                                }

                                // check for colour difference
                                var pivotRgb = pivot.Color.ToRGBValues();
                                var candidateRgb = candidate.Color.ToRGBValues();
                                if (!pivotRgb.Equals(candidateRgb))
                                {
                                    // pivot and candidate letters cannot belong to the same word 
                                    // if they don't have the same colour.
                                    return false;
                                }

                                return true;
                            }
                        };

                        var words = wordExtractor.GetWords(letters);

                        // 2. Segment page
                        var pageSegmenter = DocstrumBoundingBoxes.Instance;
                        var pageSegmenterOptions = new DocstrumBoundingBoxes.DocstrumBoundingBoxesOptions()
                        {

                        };

                        var textBlocks = pageSegmenter.GetBlocks(words);

                        // 3. Postprocessing
                        var readingOrder = UnsupervisedReadingOrderDetector.Instance;
                        var orderedTextBlocks = readingOrder.Get(textBlocks);

                        // 4. Extract text
                        foreach (var block in orderedTextBlocks)
                        {
                            sb.Append(block.Text.Normalize(NormalizationForm.FormKC)); // normalise text
                        }

                    }
                   
                    pageText = sb.ToString();
                }
            }

            return pageText;
        }

        public async Task<string> ExtractTextract(string filePath)
        {
            List<string> pdfContent = new List<string>();
            var sb = new StringBuilder();
            string pageText = "";
            string tessDataPath = System.IO.Path.Combine(AppContext.BaseDirectory, "tessdata");
            //string filePath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), file.FileName);
            //string outPath = System.IO.Path.GetTempPath() + "\\" + (Guid.NewGuid()).ToString();

            //using (var stream = System.IO.File.Create(filePath))
            //{

            //    await file.CopyToAsync(stream);
            //}

            if (System.IO.File.Exists(filePath))
            {

                //Byte[] bytes = File.ReadAllBytes(filePath);
                //String fileStream = Convert.ToBase64String(bytes);

                var readFile = File.ReadAllBytes(filePath);

                MemoryStream stream = new MemoryStream(readFile);
                AmazonTextractClient abcdclient = new AmazonTextractClient();

                AnalyzeDocumentRequest analyzeDocumentRequest = new AnalyzeDocumentRequest
                {
                    Document = new Document
                    {
                        Bytes = stream

                    },
                    FeatureTypes = new List<string>
                    {
                        FeatureType.FORMS
                    }

                };
                var analyzeDocumentResponse = await abcdclient.AnalyzeDocumentAsync(analyzeDocumentRequest);
                //Get the text blocks
                List<Block> blocks = analyzeDocumentResponse.Blocks;

                foreach (Block block in blocks)
                {
                    if (block.BlockType == BlockType.LINE)
                    {
                        if (block.Text.Length > 0)
                        {
                            sb.Append(block.Text.Normalize(NormalizationForm.FormKC)); // normalise text
                        }

                    }

                }

                pageText = sb.ToString();
            }

            return pageText;
        }


        public static Dictionary<string, string> Get_kv_relationship(List<Block> key_map, List<Block> value_map, List<Block> block_map)
        {
            List<string> kvs1 = new List<string>();
            Dictionary<string, string> kvs = new Dictionary<string, string>();
            Block value_block = new Block();
            string key, val = string.Empty;
            foreach (var block in key_map)
            {
                value_block = Find_value_block(block, value_map);
                key = Get_text(block, block_map);
                val = Get_text(value_block, block_map);
                kvs.Add(key, val);
            }

            return kvs;

        }

        public static Block Find_value_block(Block block, List<Block> value_map)
        {
            Block value_block = new Block();
            foreach (var relationship in block.Relationships)
            {
                if (relationship.Type == "VALUE")
                {
                    foreach (var value_id in relationship.Ids)
                    {
                        value_block = value_map.First(x => x.Id == value_id);
                    }

                }

            }
            return value_block;

        }

        public static string Get_text(Block result, List<Block> block_map)
        {
            string text = string.Empty;
            Block word = new Block();

            if (result.Relationships.Count > 0)
            {
                foreach (var relationship in result.Relationships)
                {
                    if (relationship.Type == "CHILD")
                    {
                        foreach (var child_id in relationship.Ids)
                        {
                            word = block_map.First(x => x.Id == child_id);
                            if (word.BlockType == "WORD")
                            {
                                text += word.Text + " ";
                            }
                            if (word.BlockType == "SELECTION_ELEMENT")
                            {
                                if (word.SelectionStatus == "SELECTED")
                                {
                                    text += "X ";
                                }

                            }
                        }
                    }
                }
            }
            return text;

        }

    }
}