const API_BASE = '/api';

export async function getApplications() {
    const response = await fetch(`${API_BASE}/applications`);

    if (!response.ok) {
        throw new Error('Failed to fetch mortgage applications');
    }

    return response.json();
}