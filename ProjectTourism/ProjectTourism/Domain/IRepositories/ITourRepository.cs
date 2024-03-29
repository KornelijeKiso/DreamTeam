﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectTourism.Domain.Model;
using ProjectTourism.Model;
using ProjectTourism.WPF.ViewModel;

namespace ProjectTourism.Domain.IRepositories
{
    public interface ITourRepository
    {
        Tour GetOne(int tourId);
        List<Tour> GetAll();
        int Add(Tour tour);
        void Delete(Tour tour);
        void Update(Tour tour);
        List<string> GetStops(Tour tour);
        Tour GetOneByTourRequest(TourRequest tourRequest);
    }
}
