namespace Tarantino
{
    public class ParameterChangeEvent : DialogEvent
    {
        public override DialogEventKind Kind => DialogEventKind.ParameterChange;
        public string Parameter { get; }
        public string Value { get; set; }

        public ParameterChangeEvent(string parameter, string value)
        {
            Parameter = parameter;
            Value = value;
        }

        public override Builder ToBuilder()
            => new Builder(DialogEventKind.ParameterChange, Parameter, Value);
    }
}
