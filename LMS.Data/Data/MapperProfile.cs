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
            CreateMap<Course, CourseDetailsViewModel>();
            CreateMap<CourseListViewModel, Course>();
            CreateMap<EditCourseViewModel, Course>().ReverseMap();
        }
    }
}
