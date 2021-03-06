﻿using System;
using NUnit.Framework;
using Shouldly;
using StoryTeller.Conversion;

namespace StoryTeller.Testing.Conversion
{
    [TestFixture]
    public class DateTime_conversion_specs
    {
        [Test]
        public void get_date_time_for_day_and_time()
        {
            DateTime date = DateTimeConverter.GetDateTime("Saturday 14:30");

            date.DayOfWeek.ShouldBe(DayOfWeek.Saturday);
            date.Date.AddHours(14).AddMinutes(30).ShouldBe(date);
            (date >= DateTime.Today).ShouldBe(true);
        }

        [Test]
        public void get_date_time_for_day_and_time_2()
        {
            DateTime date = DateTimeConverter.GetDateTime("Monday 14:30");

            date.DayOfWeek.ShouldBe(DayOfWeek.Monday);
            date.Date.AddHours(14).AddMinutes(30).ShouldBe(date);
            (date >= DateTime.Today).ShouldBe(true);
        }

        [Test]
        public void get_date_time_for_day_and_time_3()
        {
            DateTime date = DateTimeConverter.GetDateTime("Wednesday 14:30");

            date.DayOfWeek.ShouldBe(DayOfWeek.Wednesday);
            date.Date.AddHours(14).AddMinutes(30).ShouldBe(date);
            (date >= DateTime.Today).ShouldBe(true);
        }

        [Test]
        public void get_date_time_from_full_iso_8601_should_be_a_utc_datetime()
        {
            var date = DateTimeConverter.GetDateTime("2012-06-01T14:52:35.0000000Z");

            date.ShouldBe(new DateTime(2012, 06, 01, 14, 52, 35, DateTimeKind.Utc));
        }

        [Test]
        public void get_date_time_from_partial_iso_8601_uses_default_parser_and_is_local()
        {
            var date = DateTimeConverter.GetDateTime("2012-06-01T12:52:35Z");

            var gmtOffsetInHours = (int) TimeZone.CurrentTimeZone.GetUtcOffset(date).TotalHours;
            date.ShouldBe(new DateTime(2012, 06, 01, 12, 52, 35, DateTimeKind.Local).AddHours(gmtOffsetInHours));
        }

        [Test]
        public void get_date_time_from_24_hour_time()
        {
            DateTimeConverter.GetDateTime("14:30").ShouldBe(DateTime.Today.AddHours(14).AddMinutes(30));
        }

        [Test]
        public void parse_today()
        {
            DateTimeConverter.GetDateTime("TODAY").ShouldBe(DateTime.Today);
        }

        [Test]
        public void parse_today_minus_date()
        {
            DateTimeConverter.GetDateTime("TODAY-3").ShouldBe(DateTime.Today.AddDays(-3));
        }

        [Test]
        public void parse_today_plus_date()
        {
            DateTimeConverter.GetDateTime("TODAY+5").ShouldBe(DateTime.Today.AddDays(5));
        }
    }
}