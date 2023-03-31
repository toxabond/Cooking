using System.Collections.Generic;

public class SequenceBehavior : IHandler
{
    private int _index = 0;
    private readonly List<IHandler> _sequence;

    public SequenceBehavior(List<IHandler> sequence)
    {
        _sequence = sequence;
        _index = 0;
    }

    public bool Execute()
    {
        if (_sequence[_index].Execute())
        {
            NextIndex();
            return true;
        }

        return false;
    }

    private void NextIndex()
    {
        _index++;
        if (_index >= _sequence.Count)
        {
            _index = 0;
        }
    }
}