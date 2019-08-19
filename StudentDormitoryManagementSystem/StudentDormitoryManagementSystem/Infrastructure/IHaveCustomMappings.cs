using AutoMapper;

namespace StudentDormitoryManagementSystem.Infrastructure
{
    interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
