using System.Collections.Generic;
using System;

namespace MadByteLabs.Patterns
{
    /// <summary>
    /// Executes reversible commands and retains successful operations for undo and redo.
    /// </summary>
    public sealed class CommandHistory
    {
        private readonly Stack<ICommand> m_undo = new Stack<ICommand>();
        private readonly Stack<ICommand> m_redo = new Stack<ICommand>();

        /// <summary>
        /// Gets the undo count.
        /// </summary>
        public int UndoCount => m_undo.Count;

        /// <summary>
        /// Gets the redo count.
        /// </summary>
        public int RedoCount => m_redo.Count;

        /// <summary>
        /// Executes the operation. Failures propagate without rolling back partial side effects.
        /// </summary>
        /// <param name="command">The required reversible command to execute.</param>
        public void Execute(ICommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            command.Execute();
            m_undo.Push(command);
            m_redo.Clear();
        }

        /// <summary>
        /// Reverses the most recent successful operation, if one is available.
        /// </summary>
        /// <returns>True if an operation was performed; otherwise false.</returns>
        public bool Undo()
        {
            if (m_undo.Count == 0)
            {
                return false;
            }

            ICommand command = m_undo.Peek();
            command.Undo();
            m_undo.Pop();
            m_redo.Push(command);
            return true;
        }

        /// <summary>
        /// Executes the most recently undone operation again, if one is available.
        /// </summary>
        /// <returns>True if an operation was performed; otherwise false.</returns>
        public bool Redo()
        {
            if (m_redo.Count == 0)
            {
                return false;
            }

            ICommand command = m_redo.Peek();
            command.Execute();
            m_redo.Pop();
            m_undo.Push(command);
            return true;
        }

        /// <summary>
        /// Releases stored references without disposing or destroying referenced objects.
        /// </summary>
        public void Clear()
        {
            m_undo.Clear();
            m_redo.Clear();
        }
    }
}
