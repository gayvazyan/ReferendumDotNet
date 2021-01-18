using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Referendum.core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Referendum.Controllers
{
    [Route("[controller]/[action]")]
    public class FileController : Controller
    {
        private readonly ICitizenRepasitory _citizenRepasitory;
        private readonly IReferendumRepasitory _referendumRepasitory;
        private readonly ICommunityRepasitory _communityRepasitory;

        public FileController(ICitizenRepasitory citizenRepasitory,
                              IReferendumRepasitory referendumRepasitory,
                              ICommunityRepasitory communityRepasitory)
        {
            _citizenRepasitory = citizenRepasitory;
            _referendumRepasitory = referendumRepasitory;
            _communityRepasitory = communityRepasitory;
        }
        public IActionResult DownloadCitizenList(int referendumId)
        {
          

            using (var workbook = new XLWorkbook())
            {

               

                //Sheet 1
                var worksheet1 = workbook.Worksheets.Add("Քաղաքացիների ցուցակ");

                //styling
                worksheet1.Rows(1, 4).Style.Font.Bold = true;
                worksheet1.Rows(1, 4).Style.Alignment.WrapText = true;
                worksheet1.Rows(1, 4).Style.Font.FontSize = 10;
                worksheet1.Rows(1, 4).Style.Font.FontName = "GHEA Grapalat";
                worksheet1.Rows(1, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet1.Rows(1, 4).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                worksheet1.Row(1).Height = 20;
                worksheet1.Row(2).Height = 80;
                worksheet1.Row(3).Height = 20;
                worksheet1.Row(4).Height = 20;


                worksheet1.Column(1).Width = 5;
                worksheet1.Column(2).Width = 20;
                worksheet1.Column(3).Width = 20;
                worksheet1.Column(4).Width = 20;
                worksheet1.Column(5).Width = 35;
                worksheet1.Column(6).Width = 35;

              
                var referendum = _referendumRepasitory.GetByID(referendumId);


                //row1
                worksheet1.Range(worksheet1.Cell(1, 1), worksheet1.Cell(1, 6)).Merge().Value = "Հանրաքվեի հարցը";

                //row2
                worksheet1.Range(worksheet1.Cell(2, 1), worksheet1.Cell(2, 6)).Merge().Value = referendum.Question;
                worksheet1.Range(worksheet1.Cell(2, 1), worksheet1.Cell(2, 6)).Style.Font.FontColor = XLColor.FromHtml("#144192");

                //row3
                worksheet1.Range(worksheet1.Cell(3, 1), worksheet1.Cell(3, 6)).Merge().Value = "Ստորագրահավաքի տևողություն՝ "+ CommonFunctions.GetDateString(referendum.StartDate) +" - "+ CommonFunctions.GetDateString(referendum.StartDate);

               
                //row4
                var currentRow = 4;
                worksheet1.Cell(currentRow, 1).Value = "Հ/Հ";
                worksheet1.Cell(currentRow, 2).Value = "Ազգանուն";
                worksheet1.Cell(currentRow, 3).Value = "Անուն";
                worksheet1.Cell(currentRow, 4).Value = "ՀԾՀ";
                worksheet1.Cell(currentRow, 5).Value = "Միացման ամսաթիվ";
                worksheet1.Cell(currentRow, 6).Value = "Համայնք";

                var citizenList = _citizenRepasitory.GetAll().Where(p => p.ReferendumId == referendumId).ToList();

                var community = string.Empty;
                if (referendum.CommunityId!=null)
                {
                   community = _communityRepasitory.GetByID((int)referendum.CommunityId).CommunityName;
                }
                else
                {
                    community = "Համապետական հանրաքվե";
                }
                

                var number = 0;
                foreach (var item in citizenList)
                {
                    number++;
                    currentRow++;
                    worksheet1.Cell(currentRow, 1).Value = number;
                    worksheet1.Cell(currentRow, 2).Value = item.FirstName;
                    worksheet1.Cell(currentRow, 3).Value = item.LastName;
                    worksheet1.Cell(currentRow, 4).Value = item.Ssn;
                    worksheet1.Cell(currentRow, 5).Value = item.Time;
                    worksheet1.Cell(currentRow, 6).Value = community;
                }


                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Ցուցակ-" + DateTime.Now.ToString("dd MMMM yyyy HH:mm:ss") + ".xlsx");
                }
            }
        }
    }
}
