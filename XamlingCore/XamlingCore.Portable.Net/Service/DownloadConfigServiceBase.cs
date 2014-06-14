﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using XamlingCore.Portable.Contract.Downloaders;
using XamlingCore.Portable.Net.Downloaders;

namespace XamlingCore.Portable.Net.Service
{
    public abstract class DownloadConfigServiceBase : IDownloadConfigService
    {
        public string BaseUrl { get; protected set; }

        public abstract Task<IDownloadConfig> GetConfig(string url, string verb);

        public async virtual Task OnUnauthorizedResult(HttpResponseMessage result, IDownloadConfig originalConfig)
        {

        }

        public async virtual Task OnUnsuccessfulResult(HttpResponseMessage result, IDownloadConfig originalConfig)
        {

        }

        protected virtual void OnDownloadException(Exception ex, string source, IDownloadConfig originalConfig)
        {

        }

        public virtual IDownloadResult GetExceptionResult(Exception ex, string source, IDownloadConfig originalConfig)
        {
            OnDownloadException(ex, source, originalConfig);
            return new DownloadResult {DownloadException = ex, Result = null, IsSuccessCode = false};
        }

        public async virtual Task<IDownloadResult> GetResult(HttpResponseMessage result, IDownloadConfig originalConfig)
        {
            try
            {
                var resultText = "";

                var isSuccess = true;

                if (result.Content != null)
                {
                    resultText = await result.Content.ReadAsStringAsync();
                }

                if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await OnUnauthorizedResult(result, originalConfig);
                    isSuccess = false;
                }

                if (!result.IsSuccessStatusCode)
                {
                    await OnUnsuccessfulResult(result, originalConfig);
                    isSuccess = false;
                }

                return new DownloadResult
                {
                    HttpStatusCode = result.StatusCode,
                    Result = resultText,
                    IsSuccessCode = isSuccess
                };
            }
            catch (Exception ex)
            {
                return GetExceptionResult(ex, "DownloadConfigService", originalConfig);
            }
        }
    }
}
