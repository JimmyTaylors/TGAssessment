import { useEffect, useState } from 'react';
import './TodoList.css';
import TodoItem from './TodoItem';
import api from '.././API';

const userId = "abc"; // Suggestion: Can addon User Management
const initialTasks = [];

function TodoList() {
    const [tasks, setTasks] = useState(initialTasks);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [newTaskText, setNewTaskText] = useState('');


    useEffect(() => {
        api.getAll(userId)
            .then(data => {
                setTasks(data.toDoList);
                setLoading(false);
            })
            .catch(err => {
                setError('Failed to load tasks');
                setLoading(false);
            });
    }, []);

    if (loading) return <p>Loading...</p>;
    if (error) return <p>Error: {error}</p>;
    function handleInputChange(event) {
        setNewTaskText(event.target.value);
    }

    async function addTask(event) {
        event.preventDefault();
        if (!newTaskText.trim()) return;

        const newTask = { createdBy: userId, title: newTaskText, description: '' };

        try {
            const created = await api.create(newTask);
            console.log(created);
            setTasks(prev => [...prev, created.toDoInfo]);
            setNewTaskText('');
        } catch {
            setError('Could not create task');
        }
    }

    async function deleteTask(id) {
        try {
            await api.delete(id, userId);
            setTasks(prev => prev.filter(t => t.id !== id));
        } catch {
            setError('Failed to delete task');
        }
    }

    async function toggleTaskCompleted(id) {
        const task = tasks.find(t => t.id === id);
        if (!task) return;

        try {
            await api.update({
                id: task.id,
                title: task.title,
                description: task.description,
                isCompleted: !task.isCompleted,
                updatedBy: userId,
            });

            setTasks(prev =>
                prev.map(t => t.id === id ? { ...t, isCompleted: !t.isCompleted } : t)
            );
        } catch {
            setError('Failed to update status');
        }
    }

    async function updateTaskTitle(id, newTitle) {
        console.log('Updating title', id, newTitle);
        const task = tasks.find(t => t.id === id);
        if (!task) return;

        try {

            await api.update({
                id: task.id,
                title: newTitle,
                description: task.description,
                isCompleted: !task.isCompleted,
                updatedBy: userId,
            });

            setTasks(prev =>
                prev.map(t => t.id === id ? { ...t, title: newTitle } : t)
            );
        } catch {
            setError('Failed to update title');
        }
    }

    // Suggestion: API can addon Todo Indexing so that ordering can be save.
    function moveTaskUp(index) {
        if (index > 0) {
            const updatedTasks = [...tasks];
            [updatedTasks[index], updatedTasks[index - 1]] = [updatedTasks[index - 1], updatedTasks[index]];
            setTasks(updatedTasks);
        }
    }

    // Suggestion: API can addon Todo Indexing so that ordering can be save.
    function moveTaskDown(index) {
        if (index < tasks.length - 1) {
            const updatedTasks = [...tasks];
            [updatedTasks[index], updatedTasks[index + 1]] = [updatedTasks[index + 1], updatedTasks[index]];
            setTasks(updatedTasks);
        }
    }

    return (
        <article
            className="todo-list"
            aria-label="task list manager">
            <header>
                <h1>TODO</h1>
                <form
                    className="todo-input"
                    onSubmit={addTask}
                    aria-controls="todo-list">
                    <input
                        type="text"
                        required
                        autoFocus
                        placeholder="Enter a task"
                        value={newTaskText}
                        aria-label="Task text"
                        onChange={handleInputChange} />
                    <button
                        className="add-button"
                        aria-label="Add task">
                        Add
                    </button>
                </form>
            </header>
            <ol id="todo-list" aria-live="polite" aria-label="task list">
                {tasks.map((task, index) =>
                    <TodoItem
                        key={task.id}
                        taskId={task.id}
                        taskTitle={task.title}
                        taskStatus = { task.isCompleted }
                        deleteTaskCallback={() => deleteTask(task.id)}
                        moveTaskUpCallback={() => moveTaskUp(index)}
                        moveTaskDownCallback={() => moveTaskDown(index)}
                        toggleTaskStatusCallback={() => toggleTaskCompleted(task.id)}
                        updateTaskTitleCallback={updateTaskTitle}
                    />
                )}
            </ol>
        </article>
    );
}

export default TodoList;