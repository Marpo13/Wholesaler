﻿using Wholesaler.Frontend.Domain.Interfaces;
using Wholesaler.Frontend.Presentation.Exceptions;
using Wholesaler.Frontend.Presentation.States;
using Wholesaler.Frontend.Presentation.Views.Generic;
using Wholesaler.Frontend.Presentation.Views.ManagerViews.Components;

namespace Wholesaler.Frontend.Presentation.Views.ManagerViews
{
    internal class AssignTaskView : View
    {
        private readonly IUserService _service;
        private readonly IUserRepository _userRepository;
        private readonly IWorkTaskRepository _workTaskRepository;
        private readonly AssignTaskState _state;
        private readonly IUserService _service;

        public AssignTaskView(IUserService service, IWorkTaskRepository workTaskRepository, IUserRepository userRepository, ApplicationState state) : base(state)
        {
            _userRepository = userRepository;
            _workTaskRepository = workTaskRepository;
            _service = service;
            _state = State.GetManagerViews().GetAssignTask();
            _state.Initialize();
        }

        protected override async Task RenderViewAsync()
        {
            var role = State.GetLoggedInUser().Role;

            if (role != "Manager")
                throw new InvalidOperationException($"You can not assign task with role {role}. Valid role is Manager.");

            var listOfWorkTasks = await _service.GetNotAssignWorkTasks();

            if (listOfWorkTasks.IsSuccess)
                _state.AssignTasks(listOfWorkTasks.Payload);

            var workTaskComponent = new SelectWorkTaskComponent(listOfWorkTasks.Payload);
            var selectedWorkTaskId = workTaskComponent.Render().Id;

            var listOfEmployees = await _service.GetEmployees();

            if (listOfEmployees.IsSuccess)
                _state.GetEmployees(listOfEmployees.Payload);

            var userComponent = new SelectUserComponent(listOfEmployees.Payload);
            var selectedUserId = userComponent.Render().Id;

            var assignTask = await _service.AssignTask(selectedWorkTaskId, selectedUserId);

            if (assignTask.IsSuccess)
                _state.AssignTask(assignTask.Payload);

            else
                throw new InvalidDataProvidedException(assignTask.Message);

            Console.WriteLine("----------------------------");
            Console.WriteLine($"You assigned task: {assignTask.Payload.Id} to person: {assignTask.Payload.UserId}");
            Console.ReadLine();
        }        
    }
}
