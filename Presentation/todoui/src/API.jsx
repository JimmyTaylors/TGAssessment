const API_BASE_URL = 'http://localhost:5092/api/ToDo';

async function apiRequest(endpoint, method = 'GET', data = null, params = {}) {
    let url = `${API_BASE_URL}${endpoint}`;

    // Handle query params for GET/DELETE
    if (['GET', 'DELETE'].includes(method) && Object.keys(params).length > 0) {
        const queryString = new URLSearchParams(params).toString();
        url += `?${queryString}`;
    }

    const options = {
        method,
        headers: {
            'Content-Type': 'application/json',
        },
    };

    if (data && !['GET', 'DELETE'].includes(method)) {
        options.body = JSON.stringify(data);
    }

    try {
        const response = await fetch(url, options);
        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Error ${response.status}: ${errorText}`);
        }
        const result = await response.json();
        return result;
    } catch (err) {
        console.error('API Request Failed:', err);
        throw err;
    }
}

export default {
    getAll: (userId) => apiRequest('/GetAll', 'GET', null, { userId }),
    create: (task) => apiRequest('', 'POST', task),
    delete: (todoId, userId) => apiRequest('', 'DELETE', null, { todoId, userId }),
    update: (task) => apiRequest('', 'PUT', task)
};