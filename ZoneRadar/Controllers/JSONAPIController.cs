﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ZoneRadar.Models;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Repositories;
using ZoneRadar.Services;

namespace ZoneRadar.Controllers
{
    [RoutePrefix("webapi/spaces")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class JSONAPIController : ApiController
    {
        private readonly SpaceService _spaceService;
        private readonly ZONERadarRepository _repository;
        public JSONAPIController()
        {
            _spaceService = new SpaceService();
            _repository = new ZONERadarRepository();
        }

        /// <summary>
        /// 取得Filter資訊資料API(Steve)
        /// </summary>
        /// <returns></returns>
        [Route("GetFilterData")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetFilterData()
        {
            var citiesAndDistricts = _repository.GetAll<District>().GroupBy(x => x.City).OrderBy(x => x.Key.CityID);
            var spaceTypeList = _repository.GetAll<TypeDetail>().OrderBy(x => x.TypeDetailID).Select(x => x.Type);
            var amenityList = _repository.GetAll<AmenityDetail>().OrderBy(x => x.AmenityDetailID).Select(x => x.Amenity);
            var amenityIconList = _repository.GetAll<AmenityDetail>().OrderBy(x => x.AmenityDetailID).Select(x => x.AmenityICON);

            var result = new FilterViewModel
            {
                CityDistrictDictionary = citiesAndDistricts.ToDictionary(x => x.Key.CityName, x => x.Select(y => y.DistrictName).ToList()),
                SpaceTypeList = spaceTypeList.ToList(),
                AmenityList = amenityList.ToList(),
                AmenityIconList = amenityIconList.ToList(),
                SelectedCity = "",
                SelectedType = "",
                SelectedDate = ""
            };

            return Ok(result);
        }

        /// <summary>
        /// 取得搜尋頁場地資訊API(Steve)
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [Route("GetFilteredSpaces")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetFilteredSpaces(QueryViewModel query)
        {
            var queriedSpaces = _spaceService.GetFilteredSpaces(query);

            return Ok(queriedSpaces);
        }

        [Route("GetBookingCardData")]
        [AcceptVerbs("GET")]
        public IHttpActionResult GetBookingCardData(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var result = _spaceService.GetTargetBookingCard(id);
            return Ok(result);
        }
    }
}
