// Memento Pattern
namespace AdvancedDotNetPatternsDemo.Application.Patterns
{
    // Memento (state holder)
    public class EditorMemento
    {
        public string Content { get; }
        public int CursorPosition { get; }

        public EditorMemento(string content, int cursorPosition)
        {
            Content = content;
            CursorPosition = cursorPosition;
        }
    }

    // Originator (the main object that stores state)
    public class TextEditor
    {
        public string Content { get; set; } = "";
        public int CursorPosition { get; set; }

        public EditorMemento Save()
        {
            return new EditorMemento(Content, CursorPosition);
        }

        public void Restore(EditorMemento memento)
        {
            Content = memento.Content;
            CursorPosition = memento.CursorPosition;
        }

        public void Type(string text)
        {
            Content += text;
            CursorPosition += text.Length;
            Console.WriteLine($"Content: {Content} | Cursor Position: {CursorPosition}");
        }
    }

    // Caretaker (manages mementos)
    public class History
    {
        private readonly Stack<EditorMemento> _mementos = new();

        public void Save(TextEditor editor) => _mementos.Push(editor.Save());

        public void Undo(TextEditor editor)
        {
            if (_mementos.Count > 0)
            {
                var memento = _mementos.Pop();
                editor.Restore(memento);
                Console.WriteLine($"Undo performed → Content: {editor.Content}");
            }
        }
    }
}