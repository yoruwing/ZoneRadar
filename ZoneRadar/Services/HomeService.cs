﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZoneRadar.Models;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Repositories;

namespace ZoneRadar.Services
{
    public class HomeService
    {
        private readonly ZONERadarRepository _repository;
        public HomeService()
        {
            _repository = new ZONERadarRepository();
        }
        public List<SelectedSpaceViewModel> GetSelectedSpace()
        {
            var spaces = _repository.GetAll<Space>().ToList();
            var orders = _repository.GetAll<Order>().Where(x => x.OrderStatus.OrderStatusID == 2).ToList();
            var reviews = _repository.GetAll<Review>().Where(x => x.ToHost).ToList();
            var spacePhotos = _repository.GetAll<SpacePhoto>().ToList();

            var selectedSpaces = new List<SelectedSpaceViewModel>();

            foreach (var item in spaces)
            {
                //計算場地平均分數
                var spaceReview = orders.Where(x => x.SpaceID == item.SpaceID).Select(x => reviews.FirstOrDefault(y => y.OrderID == x.OrderID)).OfType<Review>().ToList();
                double scoreAvg = spaceReview.Count() == 0 ? 0 : spaceReview.Average(x => x.Score);

                //場地圖片資料表還沒建好，先寫防呆程式
                var spacePhoto = spacePhotos.FirstOrDefault(x => x.SpaceID == item.SpaceID);
                var spacePhotoUrl = spacePhoto == null ? "" : spacePhoto.SpacePhotoUrl;

                selectedSpaces.Add(
                    new SelectedSpaceViewModel
                    {
                        SpaceId = item.SpaceID,
                        CityName = item.City.CityName,
                        Capacity = item.Capacity,
                        PricePerHour = item.PricePerHour,
                        SpacePhoto = spacePhotoUrl,
                        Score = scoreAvg
                    });
            }

            var topSelectedSpaces = selectedSpaces.OrderByDescending(x => x.Score).Take(6).ToList();

            return topSelectedSpaces;
        }
        public List<ToSpaceReviewViewModel> GetSpaceReview()
        {
            var members = _repository.GetAll<Member>().ToList();
            var reviews = _repository.GetAll<Review>().Where(x => x.ToHost).ToList();
            var orders = _repository.GetAll<Order>().ToList();

            var spaceReviews = new List<ToSpaceReviewViewModel>();

            foreach (var item in reviews)
            {
                spaceReviews.Add(
                    new ToSpaceReviewViewModel
                    {
                        SpaceId = item.Order.SpaceID,
                        MemberName = members.First(x => x.MemberID == item.Order.MemberID).Name,
                        ReviewContent = item.ReviewContent,
                        Score = item.Score
                    });
            }

            var topSpaceReviews = spaceReviews.OrderByDescending(x => x.Score).Take(8).ToList();

            return topSpaceReviews;
        }
        public string GetMemberPhoto()
        {
            var members = _repository.GetAll<Member>();

            string memberPhoto = members.First().Photo;

            return memberPhoto == null ? "https://img.88icon.com/download/jpg/20200815/cacc4178c4846c91dc1bfa1540152f93_512_512.jpg!88con" : memberPhoto;
        }
        public List<SelectListItem> GetTypeOption()
        {
            var typeOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "會議", Text = "會議" },
                new SelectListItem { Value = "派對", Text = "派對" },
                new SelectListItem { Value = "聚會", Text = "聚會" },
                new SelectListItem { Value = "私人談話", Text = "私人談話" },
                new SelectListItem { Value = "課程講座", Text = "課程講座" },
                new SelectListItem { Value = "運動", Text = "運動" },
                new SelectListItem { Value = "工作", Text = "工作" },
                new SelectListItem { Value = "展覽", Text = "展覽" },
                new SelectListItem { Value = "音樂/表演", Text = "音樂/表演" },
                new SelectListItem { Value = "婚禮", Text = "婚禮" },
                new SelectListItem { Value = "拍照/攝影", Text = "拍照/攝影" },
                new SelectListItem { Value = "美容", Text = "美容" },
                new SelectListItem { Value = "烹飪", Text = "烹飪" },
                new SelectListItem { Value = "儲物", Text = "儲物" },
                new SelectListItem { Value = "發表會", Text = "發表會" },
                new SelectListItem { Value = "親子活動", Text = "親子活動" },
                new SelectListItem { Value = "其他", Text = "其他" }
            };

            return typeOptions;
        }
        public List<SelectListItem> GetCityOption()
        {
            var cities = _repository.GetAll<City>().ToList();
            var cityOptions = cities.Select(x => new SelectListItem
            {
                Value = x.CityID.ToString(),
                Text = x.CityName
            }).ToList();

            return cityOptions;
        }
        public void DisposeCtx()
        {
            _repository.Dispose();
        }
        //public void AddTestMethod()
        //{
        //    List<Space> spacesss = new List<Space>
        //    {
        //        new Space{ MemberID = 2238, SpaceName = "藝術大學地下室", Introduction = "abc", MeasureOfArea = 30, Capacity = 20, PricePerHour = 350, MinHours = 4, Discount = "[{ \"hours\": \"8\", \"Discount\": \"0.1\"}]", CountryID = 1, CityID = 2, DistrictID = 2, Address = "address", Latitude = "34", Longitude = "121", CleaningProtocol = "sde", HostRules = "rgtt", Traffic = "jdei", ShootingEquipment = "dwede", Cancellation = "標準30天", Parking = "ewdwe", Type = "mdkedd"
        //        },
        //        new Space{ MemberID = 2238, SpaceName = "會議廳", Introduction = "nuju", MeasureOfArea = 55, Capacity = 70, PricePerHour = 600, MinHours = 3, Discount = "[{ \"hours\": \"8\", \"Discount\": \"0.1\"}]", CountryID = 1, CityID = 2, DistrictID = 3, Address = "addressjuju", Latitude = "36", Longitude = "122", CleaningProtocol = "jujuj", HostRules = "rujjuj", Traffic = "jdujuei", ShootingEquipment = "dweyude", Cancellation = "標準90天", Parking = "ewdujuwe", Type = "mdujkedd"
        //        }
        //    };
        //    List<Orders> ordersss = new List<Orders>
        //    {
        //        new Orders{ OrderNumber = 212313, SpaceID = 246, MemberID = 1756, Paid = true, PaymentDate = new DateTime(2021, 9, 21), CancelDateTime = null, ContactName = "匿名螞蟻", ContactPhone = "0988888888", OrderStatus = 1},
        //        new Orders{ OrderNumber = 212436, SpaceID = 247, MemberID = 1756, Paid = true, PaymentDate = new DateTime(2021, 9, 18), CancelDateTime = null, ContactName = "匿名長頸鹿", ContactPhone = "0988888888", OrderStatus = 1}
        //    };
        //    List<SpacePhoto> spacePhotosss = new List<SpacePhoto>
        //    {
        //        new SpacePhoto{ SpaceID = 246, SpacePhotoUrl = "https://picsum.photos/id/236/1220/800"},
        //        new SpacePhoto{ SpaceID = 247, SpacePhotoUrl = "https://picsum.photos/id/235/1400/1100"},
        //    };


        //    _repository.CreateRange(spacePhotosss);
        //    _repository.SaveChanges();
        //    _repository.Dispose();
        //}
        public void TestMethod()
        {
            List<SpacePhoto> spacePhotos = new List<SpacePhoto>
            {
                new SpacePhoto{SpaceID = 45, SpacePhotoUrl = "https://picsum.photos/1980/1200/?random=1"},
                new SpacePhoto{SpaceID = 45, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=2"},
                new SpacePhoto{SpaceID = 45, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=3"},
                new SpacePhoto{SpaceID = 45, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=4"},
                new SpacePhoto{SpaceID = 45, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=5"},
                new SpacePhoto{SpaceID = 45, SpacePhotoUrl = "https://picsum.photos/1000/800/?random=6"},
                new SpacePhoto{SpaceID = 45, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=7"},
                new SpacePhoto{SpaceID = 45, SpacePhotoUrl = "https://picsum.photos/400/300/?random=8"},
                new SpacePhoto{SpaceID = 45, SpacePhotoUrl = "https://picsum.photos/800/600/?random=9"},
                new SpacePhoto{SpaceID = 45, SpacePhotoUrl = "https://picsum.photos/1200/800/?random=10"},
                new SpacePhoto{SpaceID = 60, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=11"},
                new SpacePhoto{SpaceID = 60, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=12"},
                new SpacePhoto{SpaceID = 60, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=13"},
                new SpacePhoto{SpaceID = 60, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=14"},
                new SpacePhoto{SpaceID = 82, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=15"},
                new SpacePhoto{SpaceID = 82, SpacePhotoUrl = "https://picsum.photos/500/700/?random=16"},
                new SpacePhoto{SpaceID = 82, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=17"},
                new SpacePhoto{SpaceID = 89, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=18"},
                new SpacePhoto{SpaceID = 89, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=19"},
                new SpacePhoto{SpaceID = 89, SpacePhotoUrl = "https://picsum.photos/500/700/?random=20"},
                new SpacePhoto{SpaceID = 139, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=21"},
                new SpacePhoto{SpaceID = 139, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=22"},
                new SpacePhoto{SpaceID = 139, SpacePhotoUrl = "https://picsum.photos/500/700/?random=23"},
                new SpacePhoto{SpaceID = 139, SpacePhotoUrl = "https://picsum.photos/300/400/?random=24"},
                new SpacePhoto{SpaceID = 157, SpacePhotoUrl = "https://picsum.photos/300/400/?random=25"},
                new SpacePhoto{SpaceID = 157, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=26"},
                new SpacePhoto{SpaceID = 157, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=27"},
                new SpacePhoto{SpaceID = 160, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=28"},
                new SpacePhoto{SpaceID = 160, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=29"},
                new SpacePhoto{SpaceID = 160, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=30"},
                new SpacePhoto{SpaceID = 160, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=31"},
                new SpacePhoto{SpaceID = 160, SpacePhotoUrl = "https://picsum.photos/1200/800/?random=32"},
                new SpacePhoto{SpaceID = 163, SpacePhotoUrl = "https://picsum.photos/1200/800/?random=33"},
                new SpacePhoto{SpaceID = 163, SpacePhotoUrl = "https://picsum.photos/1200/800/?random=34"},
                new SpacePhoto{SpaceID = 163, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=35"},
                new SpacePhoto{SpaceID = 168, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=36"},
                new SpacePhoto{SpaceID = 168, SpacePhotoUrl = "https://picsum.photos/1000/900/?random=37"},
                new SpacePhoto{SpaceID = 168, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=38"},
                new SpacePhoto{SpaceID = 168, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=39"},
                new SpacePhoto{SpaceID = 171, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=40"},
                new SpacePhoto{SpaceID = 171, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=41"},
                new SpacePhoto{SpaceID = 171, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=42"},
                new SpacePhoto{SpaceID = 173, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=43"},
                new SpacePhoto{SpaceID = 173, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=44"},
                new SpacePhoto{SpaceID = 173, SpacePhotoUrl = "https://picsum.photos/1920/1080/?random=45"}
            };
            _repository.CreateRange(spacePhotos);
            _repository.SaveChanges();
            _repository.Dispose();
        }
        //public List<Space> GetSpace(int? id)
        //{
        //    var space = spaces.Where(x => x.SpaceID == id).ToList();
        //    return space;
        //}
    }
}