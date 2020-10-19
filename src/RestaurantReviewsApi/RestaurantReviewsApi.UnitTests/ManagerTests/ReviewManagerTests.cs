﻿using RestaurantReviewsApi.ApiModels;
using RestaurantReviewsApi.Bll.Managers;
using RestaurantReviewsApi.Bll.Translators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace RestaurantReviewsApi.UnitTests.ManagerTests
{
    public class ReviewManagerTests : AbstractTestHandler
    {
        private IApiModelTranslator _translator => new ApiModelTranslator();

        [Fact]
        public async void CanDeleteReview()
        {
            var guid = AddRestaurant();
            var reviewGuid = AddReview(guid);
            var manager = new ReviewManager(null, DbContext, _translator);
            var delete = await manager.DeleteReviewAsync(reviewGuid);
            Assert.True(delete);

            var review = DbContext.Review.FirstOrDefault(x => x.ReviewId == reviewGuid);
            Assert.True(review.IsDeleted);
        }

        [Fact]
        public async void CanGetReview()
        {
            var guid = AddRestaurant();
            var reviewGuid = AddReview(guid);
            var manager = new ReviewManager(null, DbContext, _translator);
            var review = await manager.GetReviewAsync(reviewGuid);
            Assert.NotNull(review);
        }

        [Fact]
        public async void CanPostReview()
        {
            var guid = AddRestaurant();
            var model = ApiModelHelperFunctions.RandomReviewApiModel(guid);
            var manager = new ReviewManager(null, DbContext, _translator);
            var post = await manager.PostReviewAsync(model);
            Assert.True(post);
        }

        [Fact]
        public async void CanSearchReviewByRestaurantId()
        {
            var guid = AddRestaurant();
            var reviewGuid = AddReview(guid);

            ReviewSearchApiModel searchModel = new ReviewSearchApiModel()
            {
                RestaurantId = guid
            };

            var manager = new ReviewManager(null, DbContext, _translator);
            var resultSet = await manager.SearchReviewsAsync(searchModel);
            var result = resultSet.First();

            Assert.Equal(1, resultSet.Count);
            Assert.Equal(guid, result.RestaurantId);
            Assert.Equal(reviewGuid, result.ReviewId);
        }

        [Fact]
        public async void CanSearchReviewByUserName()
        {
            var guid = AddRestaurant();
            var userName = HelperFunctions.RandomString(10);
            var reviewGuid = AddReview(guid, userName);

            ReviewSearchApiModel searchModel = new ReviewSearchApiModel()
            {
                UserName = userName
            };

            var manager = new ReviewManager(null, DbContext, _translator);
            var resultSet = await manager.SearchReviewsAsync(searchModel);
            var result = resultSet.First();

            Assert.Equal(1, resultSet.Count);
            Assert.Equal(guid, result.RestaurantId);
            Assert.Equal(reviewGuid, result.ReviewId);
        }

        [Fact]
        public async void CanSearchReviewByUserNameAndRestaurantId()
        {
            var guid = AddRestaurant();
            var userName = HelperFunctions.RandomString(10);
            var reviewGuid = AddReview(guid, userName);

            ReviewSearchApiModel searchModel = new ReviewSearchApiModel()
            {
                UserName = userName,
                RestaurantId = guid
            };

            var manager = new ReviewManager(null, DbContext, _translator);
            var resultSet = await manager.SearchReviewsAsync(searchModel);
            var result = resultSet.First();

            Assert.Equal(1, resultSet.Count);
            Assert.Equal(guid, result.RestaurantId);
            Assert.Equal(reviewGuid, result.ReviewId);
        }
    }
}
