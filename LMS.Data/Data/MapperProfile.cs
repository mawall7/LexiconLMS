using AutoMapper;
using LMS.Core.Entities;
using LMS.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Data.Data
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateCourseViewModel, Course>();
            CreateMap<CourseDetailsViewModel, Course>();
            CreateMap<CourseListViewModel, Course>();
        }
    }
}
