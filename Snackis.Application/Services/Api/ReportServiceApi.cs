using Snackis.Application.DTOs;
using Snackis.Application.Interfaces.Api;
using Snackis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace Snackis.Application.Services.Api
{
    public class ReportServiceApi : IReportServiceApi
    {
        private readonly HttpClient _httpClient;

        public ReportServiceApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private static ApplicationReport MapToApplication(ReportDTO report) => new()
        {
            Id = report.Id,
            Reason = report.Reason,
            ReportedAt = report.ReportedAt,
            PostId = report.PostId,
            TopicId = report.TopicId,
            ReporterUserId = report.ReporterUserId,
            IsResolved = report.IsResolved,
            Name = report.ReporterUserName
        };
        public async Task<List<ApplicationReport>> GetAllReportsAsync()
        {
            var dtos = await _httpClient.GetFromJsonAsync<List<ReportDTO>>("api/report");
            if (dtos == null)
                return new List<ApplicationReport>();

            return dtos.Select(MapToApplication).ToList();
        }

        public async Task<ApplicationReport?> GetReportByIdAsync(int id)
        {
            var dto = await _httpClient.GetFromJsonAsync<ReportDTO>($"api/report/{id}");
            if (dto == null)
                return null;

            return MapToApplication(dto);
        }

        public async Task CreateReportAsync(ApplicationReport report)
        {
            var dto = new CreateReportDTO
            {
                Reason = report.Reason,
                PostId = report.PostId,
                TopicId = report.TopicId,
                ReporterUserId = report.ReporterUserId
            };

            var response = await _httpClient.PostAsJsonAsync("api/report", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateReportAsync(ApplicationReport report)
        {
            var dto = new ReportDTO
            {
                Id = report.Id,
                Reason = report.Reason,
                PostId = report.PostId,
                TopicId = report.TopicId,
                IsResolved = report.IsResolved
            };

            var response = await _httpClient.PutAsJsonAsync($"api/report/{report.Id}", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteReportAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/report/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
