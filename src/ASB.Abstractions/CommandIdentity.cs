namespace ASB.Abstractions
{
    public class CommandIdentity
    {
        public CommandIdentity(string serviceId, string commandId)
        {
            Value = serviceId + "-" + commandId;
        }

        private CommandIdentity(string value)
        {
            Value = value;
        }
        
        public string Value { get; }

        protected bool Equals(CommandIdentity other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CommandIdentity) obj);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public static implicit operator string(CommandIdentity identity) => identity.Value;
        public static implicit operator CommandIdentity(string identity) => new CommandIdentity(identity);
    }
}