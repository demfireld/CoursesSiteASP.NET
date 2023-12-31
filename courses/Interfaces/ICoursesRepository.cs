﻿using courses.Models;

namespace courses.Interfaces
{
    public interface ICoursesRepository
    {
        Task<IEnumerable<Courses>> GetAllAsync();
        Task<Courses> GetByIdAsync(int id);
        bool Add(Courses courses);
        bool Update(Courses courses);
        bool Delete(Courses courses);
        bool Save();
    }
}
