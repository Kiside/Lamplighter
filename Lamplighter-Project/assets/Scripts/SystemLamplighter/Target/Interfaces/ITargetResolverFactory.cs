using SystemLamplighter.DataStructure.GeneralData;
using SystemLamplighter.Target.Interfaces;


using SystemLamplighter.Common.Enums;

public interface ITargetResolverFactory
{
	ITargetResolver Create(TargetType targetType);
}