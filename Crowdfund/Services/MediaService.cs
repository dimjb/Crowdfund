﻿using Crowdfund.Data;
using Crowdfund.Models;
using Crowdfund.Services.CreateOptions;
using Crowdfund.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;


namespace Crowdfund.Services
{
    public class MediaService : IMediaService
    {
        private readonly DataContext _context;

        public MediaService(DataContext context)
        {
            _context = context;
        }

        public Media CreateMedia(CreateMediaOptions options)
        {
            if (options == null || !Enum.IsDefined(typeof(MediaType), options.MediaTypeId) ||
                string.IsNullOrWhiteSpace(options.MediaUrl))
            {
                return null;
            }

            var media = new Media
            {
                MediaType = (MediaType) options.MediaTypeId
            };
            
            var urlChecking = options.MediaUrl;
            
            if (media.MediaType == (MediaType) MediaType.Video)
            {
                urlChecking = urlChecking.Trim();
                if (urlChecking.Contains("youtube.com"))
                {
                    media.MediaUrl = urlChecking;
                    Console.WriteLine("valid Video");
                }
                else
                {
                    Console.WriteLine("Not valid Video");
                    return null;
                }
            }
            else
            {
                media.MediaUrl = urlChecking;
                Console.WriteLine("Image");
            }

            return media;
        }

        public bool DeleteMedia(Media mediaToDelete)
        {
            _context.Remove(mediaToDelete);
            return _context.SaveChanges() > 0;
        }
    }
}