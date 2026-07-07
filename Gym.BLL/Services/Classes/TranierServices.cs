using Gym.BLL.Services.Interfaces;
using Gym.BLL.ViewModels.TrainerViewModels;
using Gym.DAL.Entities;
using Gym.DAL.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.BLL.Services.Classes
{
    public class TranierServices : ITranierServices
    {
        private readonly IGenaricRepo<Traniner> _trainerRepo;
        private readonly IGenaricRepo<Session> _sessionRepo;

        public TranierServices(IGenaricRepo<Traniner> _trainerRepo,IGenaricRepo<Session> _sessionRepo)
        {
            this._trainerRepo = _trainerRepo;
            this._sessionRepo = _sessionRepo;
        }


        public async Task<IEnumerable<TrainerViewModel>> GetAllTrainerAsync(CancellationToken ct = default)
        {
            var trainers = await _trainerRepo.GetAll(false, ct);
            if (!trainers.Any()) return [];
            var trainerViewModels = trainers.Select(t => new TrainerViewModel
            {
                Id = t.Id,
                Name = t.Name,
                Email = t.Email,
                Phone = t.Phone,
                DateOfBirth = t.DateOfBirth.ToString("yyyy-MM-dd"),
                Specialties = t.Specialties.ToString(),
            });
            return trainerViewModels;
        }

        public async Task<TrainerViewModel?> GetTrainerDetailsAsync(int id, CancellationToken ct = default)
        {
            var trainer = await _trainerRepo.GetById(id, ct);
            if(trainer is null) return null;
            var trainerViewModel = new TrainerViewModel
            {
                Id = trainer.Id,
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                DateOfBirth = trainer.DateOfBirth.ToString("yyyy-MM-dd"),
                Address=$"{trainer.Address?.BuildNumber} - {trainer.Address?.Street} - {trainer.Address?.City}"
            };
            return trainerViewModel;

        }

        public async Task<TrainerToUpdateViewModel> GetTrainerToUpdateAsync(int id, CancellationToken ct = default)
        {
            var trainer = await _trainerRepo.GetById(id, ct);
            if(trainer is null) return null;
            return new TrainerToUpdateViewModel ()
            {
                Name = trainer.Name,
                Email = trainer.Email,
                Phone = trainer.Phone,
                BuildingNumber = trainer.Address?.BuildNumber ?? 0,
                City = trainer.Address?.City ?? string.Empty,
                Street = trainer.Address?.Street ?? string.Empty,
                Specialties = trainer.Specialties
            };
        }



        public async Task<bool> CreateTrainerAsync(CreateTrainerViewModel model, CancellationToken ct = default)
        {
            var existingEmail = await _trainerRepo.AnyAsync(t => t.Email == model.Email,ct);
            var existingPhone = await _trainerRepo.AnyAsync(t => t.Phone == model.Phone,ct);
            if (existingEmail || existingPhone) return false;
            var tranier = new Traniner()
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                DateOfBirth = model.DateOfBirth,
                Specialties = model.Specialties,
                HiringDate = DateTime.Now,
                Address = new Address
                {
                    BuildNumber = model.BuildingNumber,
                    City = model.City,
                    Street = model.Street
                },
                Gender= model.Gender,
               


            };
             _trainerRepo.Add(tranier);
            var result = await _trainerRepo.SaveChangesAsync(ct);

            return result > 0;
        }

        public async Task<bool> UpdateTrainerDetailsAsync(int id, TrainerToUpdateViewModel model, CancellationToken ct = default)
        {
            var trainer = await _trainerRepo.GetById(id, ct);
            if (trainer is null) return false;
            if (await _trainerRepo.AnyAsync(t => t.Email == model.Email && t.Id != id)) return false;
            if (await _trainerRepo.AnyAsync(t => t.Phone == model.Phone && t.Id != id)) return false;

            trainer.Email = model.Email;
            trainer.Phone = model.Phone;
            trainer.Specialties = model.Specialties;
            trainer.Address?.BuildNumber = model.BuildingNumber;
            trainer.Address?.Street = model.Street;
            trainer.Address?.City = model.City;

            _trainerRepo.update(trainer);
            var result = await _trainerRepo.SaveChangesAsync(ct);
            return result > 0;                
        }
        public async Task<bool> DeleteTrainerDetailsAsync(int id, CancellationToken ct = default)
        {
            var sessions= await _sessionRepo.AnyAsync(s=>s.TrainerId == id);
            if (sessions) return false;
            _trainerRepo.Delete(id);
            var result = await _trainerRepo.SaveChangesAsync(ct);
            return result > 0;
        }
    }
}
