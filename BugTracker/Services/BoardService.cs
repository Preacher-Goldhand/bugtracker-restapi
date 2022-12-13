﻿using AutoMapper;
using BugTracker.Entities;
using BugTracker.Middleware.CustomErrors;
using BugTracker.Models;
using BugTracker.Models.CreateDtos;
using BugTracker.Models.UpdateDtos;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Services
{
    public interface IBoardService
    {
        int Create(CreateBoardDto dto);

        IEnumerable<BoardDto> GetAll();

        BoardDto GetById(int id);

        void Update(int id, UpdateBoardDto dto);

        void Delete(int id);
    }

    public class BoardService : IBoardService
    {
        private readonly BugTrackerDbContext _dbContext;
        private readonly IMapper _mapper;

        public BoardService(BugTrackerDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public int Create(CreateBoardDto dto)
        {
            if (_dbContext
                .Boards
                .Any(b => b.Name == dto.Name))
            {
                throw new AlreadyExistsException("Board already exists");
            }

            var board = _mapper.Map<Board>(dto);
            _dbContext.Boards.Add(board);
            _dbContext.SaveChanges();
            return board.Id;
        }

        public IEnumerable<BoardDto> GetAll()
        {
            var boards = _dbContext
               .Boards
               .Include(b => b.BoardTasks)
               .ToList();

            var boardDtos = _mapper.Map<List<BoardDto>>(boards);
            return boardDtos;
        }

        public BoardDto GetById(int id)
        {
            var board = _dbContext
               .Boards
               .Include(b => b.BoardTasks)
               .FirstOrDefault(b => b.Id == id);

            if (board == null)
                throw new NotFoundException("Board not found");

            var boardDto = _mapper.Map<BoardDto>(board);
            return boardDto;
        }

        public void Update(int id, UpdateBoardDto dto)
        {
            var board = _dbContext
                .Boards
                .FirstOrDefault(b => b.Id == id);

            if (board == null)
                throw new NotFoundException("Board not found");

            board.Name = dto.Name;

            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var board = _dbContext
              .Boards
              .FirstOrDefault(b => b.Id == id);

            if (board == null)
                throw new NotFoundException("Board not found");

            _dbContext.Boards.Remove(board);
            _dbContext.SaveChanges();
        }
    }
}