/************************************************************************************************
 *
 * 创建：zhangxh-a 2012-8-27
 * 功能：删除方案
 *
 * Copyright (c) 1998-2012 Glodon Corporation
 *
 *************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Justin.FrameWork.Web.Extensions;

namespace GTP.BI.WebIntegration.AjaxServices
{
    public class DeleteSolution : ServiceBase
    {
        private long SolutionId { get; set; }

        public override void Initialize()
        {
            SolutionId = Request.GetValue<long>("solutionId");     
        }

        public override object Execute()
        {
            bool b = false;// SolutionRepository.DeleteSolution(SolutionId);
            return b;
        }
    }
}