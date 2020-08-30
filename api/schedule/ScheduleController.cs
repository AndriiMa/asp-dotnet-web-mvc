using Microsoft.AspNetCore.Mvc;

namespace csharp_mvc
{

    [Route("/lists")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private ScheduleRepository scheduleRepository = ScheduleRepository.GetInstance();
        private ScheduleMapper scheduleMapper = new ScheduleMapper();

        [HttpGet("/{id}")]
        public ScheduleDto GetScheduleById(int id)
        {
            Schedule schedule = scheduleRepository.GetById(id);
            return scheduleMapper.MapToDto(schedule);
        }


    }

}