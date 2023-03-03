namespace CharacterCopierKata
{
    public class Copier
    {
        private ISource _source;
        private IDestination _destination;
        public Copier(ISource source, IDestination destination)
        {
            _source = source;
            _destination = destination;
        }

        public void Copy()
        {
            char Char = _source.GetChar();
            if (Char != '\n')
            {
                _destination.SetChar(Char);
            }
        }
        }
    }