﻿using BoldReports.Web.ReportViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ReportServices.Controllers.docs
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PasswordProtectReportsController : ApiController, IReportController
    {
        //Post action for processing the rdl/rdlc report 
        public object PostReportAction(Dictionary<string, object> jsonResult)
        {
            return ReportHelper.ProcessReport(jsonResult, this);
        }

        //Get action for getting resources from the report
        [System.Web.Http.ActionName("GetResource")]
        [AcceptVerbs("GET")]
        public object GetResource(string key, string resourcetype, bool isPrint)
        {
            return ReportHelper.GetResource(key, resourcetype, isPrint);
        }

        //Method will be called when initialize the report options before start processing the report        
        public void OnInitReportOptions(ReportViewerOptions reportOption)
        {
            var resourcesPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Scripts");

            reportOption.ReportModel.ExportResources.Scripts = new List<string>
            {
                resourcesPath + @"\bold-reports\common\ej.reporting.common.min.js",
                resourcesPath + @"\bold-reports\common\ej.reporting.widgets.min.js",
                //Chart component script
                resourcesPath + @"\bold-reports\data-visualization\ej.chart.min.js",
                //Gauge component scripts
                resourcesPath + @"\bold-reports\data-visualization\ej.lineargauge.min.js",
                resourcesPath + @"\bold-reports\data-visualization\ej.circulargauge.min.js",
                //Map component script
                resourcesPath + @"\bold-reports\data-visualization\ej.map.min.js",
                //Report Viewer Script
                resourcesPath + @"\bold-reports\ej.report-viewer.min.js"
            };

            reportOption.ReportModel.ExportResources.DependentScripts = new List<string>
            {
                resourcesPath + @"\dependent\jquery.min.js"
            };

            //PDF encryption
            reportOption.ReportModel.PDFOptions = new BoldReports.Writer.PDFOptions();
            reportOption.ReportModel.PDFOptions.Security = new Syncfusion.Pdf.Security.PdfSecurity()
            {
                UserPassword = "password"
            };

            //Word encryption
            reportOption.ReportModel.WordOptions = new BoldReports.Writer.WordOptions()
            {
                EncryptionPassword = "password"
            };

            //Excel encryption

            reportOption.ReportModel.ExcelOptions = new BoldReports.Writer.ExcelOptions()
            {
                PasswordToModify = "password",
                PasswordToOpen = "password"
            };
            //PPT encryption
            reportOption.ReportModel.PPTOptions = new BoldReports.Writer.PPTOptions()
            {
                EncryptionPassword = "password"
            };
        }

        //Method will be called when reported is loaded
        public void OnReportLoaded(ReportViewerOptions reportOption)
        {
            //You can update report options here
        }
    }
}