import { useState } from 'react';
import PropTypes from 'prop-types';

function TodoItem({ taskId, taskTitle, taskStatus, deleteTaskCallback, moveTaskUpCallback, moveTaskDownCallback, toggleTaskStatusCallback, updateTaskTitleCallback }) {

    const [editableTitle, setEditableTitle] = useState(taskTitle);

    return (
        <li aria-label="task">
            <input
                type="checkbox"
                checked={taskStatus}
                onChange={() => toggleTaskStatusCallback(taskId)}
                aria-label="Mark task as completed"
            />

            <input
                type="text"
                value={editableTitle}
                onChange={(e) => setEditableTitle(e.target.value)}
                onBlur={() => updateTaskTitleCallback(taskId, editableTitle)}
                className={`task-input ${taskStatus ? 'completed' : ''}`}
                style={{ fontSize: '16px', flex: 1 }}
                aria-label="Edit task title"
            />

            {/* Button group container */}
            <div className="task-actions">
                <button
                    type="button"
                    aria-label="Delete task"
                    className="delete-button"
                    onClick={deleteTaskCallback}>
                    Del
                </button>
                <button
                    type="button"
                    aria-label="Move task up"
                    className="up-button"
                    onClick={moveTaskUpCallback}>
                    Up
                </button>
                <button
                    type="button"
                    aria-label="Move task down"
                    className="down-button"
                    onClick={moveTaskDownCallback}>
                    Down
                </button>
            </div>
        </li>
    );
}

TodoItem.propTypes = {
    taskId: PropTypes.oneOfType([PropTypes.string, PropTypes.number]).isRequired,
    taskTitle: PropTypes.string.isRequired,
    taskStatus: PropTypes.bool.isRequired,
    deleteTaskCallback: PropTypes.func.isRequired,
    moveTaskUpCallback: PropTypes.func.isRequired,
    moveTaskDownCallback: PropTypes.func.isRequired,
    toggleTaskStatusCallback: PropTypes.func.isRequired,
    updateTaskTitleCallback: PropTypes.func.isRequired,
};

export default TodoItem;