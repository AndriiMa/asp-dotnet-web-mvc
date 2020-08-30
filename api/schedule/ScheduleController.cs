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

        [HttpDelete("/{id}")]
        public void DeleteScheduleById(int id)
        {
            scheduleRepository.DeleteById(id);
        }

        [HttpPost("")]
        public ScheduleDto SaveNewSchedule([FromBody] ScheduleDto dto)
        {
            Schedule newSchedule = scheduleMapper.MapToObject(dto);
            Schedule savedSchedule = scheduleRepository.SaveNew(newSchedule);
            return scheduleMapper.MapToDto(savedSchedule);
        }

        [HttpPost("/{id}")]
        public void UpdateScheduleById(int id, [FromBody] ScheduleDto dto)
        {
            Schedule schedule = scheduleMapper.MapToObject(dto);
            scheduleRepository.Update(schedule);
        }


    }

}