﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZoneRadar.Models;
using ZoneRadar.Models.ViewModels;
using ZoneRadar.Repositories;

namespace ZoneRadar.Services
{
    public class PreOrderService
    {
        private readonly ZONERadarRepository _repository;
        public PreOrderService()
        {
            _repository = new ZONERadarRepository();
        }
        public List<ShopCarViewModel> GetShopCarVM(int id)
        {
            var result = new List<ShopCarViewModel>();

            var spaces = _repository.GetAll<Space>().ToList();
            var orders = _repository.GetAll<Order>().ToList();
            var members = _repository.GetAll<Member>().ToList();
            var orderdetails = _repository.GetAll<OrderDetail>().ToList();
            var spacepics = _repository.GetAll<SpacePhoto>().ToList();
            var spacediscounts = _repository.GetAll<SpaceDiscount>().ToList();

            //帶入會員ID
            var orderformember25 = orders.Where(x => x.MemberID == id && x.OrderStatusID == 1);

            //在所有訂單中符合此會員ID
            foreach (var item in orderformember25)
            {
                //該場地名稱
                var spacename = spaces.FirstOrDefault(x => x.SpaceID == item.SpaceID).SpaceName;
                //該場地照片
                var spacepic = spacepics.FirstOrDefault(x => x.SpaceID == item.SpaceID).SpacePhotoUrl;
                //該場地主姓名
                var ownername = members.FirstOrDefault(x => x.MemberID == item.Space.MemberID).Name;
                //該場地主電話
                var ownerphone = members.FirstOrDefault(x => x.MemberID == item.Space.MemberID).Phone;

                decimal money = 0;
                var temp = new List<RentDetailViewModel>();
                //訂單時間及人數
                foreach (var rentdetail in item.OrderDetail)
                {
                    var renttime = rentdetail.StartDateTime;
                    var rentbacktime = rentdetail.EndDateTime;
                    var people = rentdetail.Participants;
                    var detailid = rentdetail.OrderDetailID;

                    //算金額
                    var totalhour = rentbacktime.Subtract(renttime).TotalHours;
                    var discounthour = spacediscounts.FirstOrDefault(x => x.SpaceID == item.SpaceID).Hour;
                    var discount = spacediscounts.FirstOrDefault(x => x.SpaceID == item.SpaceID).Discount;
                    var spacehourofmoney = spaces.FirstOrDefault(x => x.SpaceID == item.SpaceID).PricePerHour;
                    decimal moneytemp;
                    if (totalhour > discounthour)
                    {
                        moneytemp = (spacehourofmoney * (int)totalhour) * (1 - discount);
                    }
                    else
                    {
                        moneytemp = spacehourofmoney * (int)totalhour;
                    }

                    money += moneytemp;
                    moneytemp = 0;
                    temp.Add(new RentDetailViewModel
                    {
                        RentTime = renttime.ToString("yyyy-MM-dd HH:mm"),
                        RentBackTime = rentbacktime.ToString("yyyy-MM-dd HH:mm"),
                        People = people,
                        OrderDetailId = detailid,
                        OrderId = rentdetail.OrderID
                    });
                }



                result.Add(new ShopCarViewModel
                {
                    SpaceName = spacename,
                    SpaceUrl = spacepic,
                    OwnerName = ownername,
                    OwnerPhone = ownerphone,
                    Money = money,
                    RentDetailVM = temp,
                    OrderID = item.OrderID
                });


            }


            return result;
        }
        public OrderDetail EditShopCarDetail(RentDetailViewModel model)
        {
            var orderDetail = new OrderDetail();

            orderDetail.OrderID = model.OrderId;
            orderDetail.StartDateTime = DateTime.Parse(model.RentTime);
            orderDetail.EndDateTime = DateTime.Parse(model.RentBackTime);
            orderDetail.OrderDetailID = model.OrderDetailId;
            orderDetail.Participants = model.People;
            if (orderDetail.OrderDetailID == model.OrderDetailId)
            {
                _repository.Update<OrderDetail>(orderDetail);
                _repository.SaveChanges();
            }
            return orderDetail;
        }
        
        public OrderDetail DeleteShopCarDetail(int id)
        {
            //OrderDetail orderdetail = OrderDetail.Find(id);
            var orderdetails = _repository.GetAll<OrderDetail>().ToList();
            var orderdetail = orderdetails.FirstOrDefault(x => x.OrderDetailID == id);


            _repository.Delete<OrderDetail>(orderdetail);
            _repository.SaveChanges();


            return orderdetail;
        }
        public Order DeleteShopCarOrder(int id)
        {
            var orders = _repository.GetAll<Order>().ToList();
            var orderdetails = _repository.GetAll<OrderDetail>().ToList();
            var orderdetail = orderdetails.FindAll(x => x.OrderID == id);
            foreach(var item in orderdetail)
            {
                if(item != null)
                {
                    _repository.Delete<OrderDetail>(item);
                    _repository.SaveChanges();
                }
            }
            var order = orders.FirstOrDefault(x => x.OrderID == id);

            _repository.Delete<Order>(order);
            _repository.SaveChanges();

            return order;
        }
    }
}