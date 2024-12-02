const API_URL = 'http://localhost:5290/api';

export const fetchCandidates = async () => {
  const response = await fetch(`${API_URL}/Candidates`);
  if (!response.ok) {
    throw new Error('Failed to fetch candidates');
  }
  return await response.json();
};

export const fetchJobVacancies = async (page, limit) => {
  try {
    const response = await fetch(`${API_URL}/jobvacancy?offset=${page}&limit=${limit}`);
    if (!response.ok) {
      throw new Error(`Failed to fetch job vacancies: ${response.status}`);
    }
    return await response.json();
  } catch (error) {
    console.error('Error fetching job vacancies:', error.message);
    throw error;
  }
};

export const addJobVacancy = async (data) => {
  try {
    const response = await fetch(`${API_URL}/jobvacancy`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
    });
    
    if (!response.ok) {
      throw new Error(`Failed to add job vacancy: ${response.status}`);
    }
    return await response.json();
  } catch (error) {
    console.error('Error adding job vacancy:', error.message);
    throw error;
  }
};

export const updateJobVacancy = async (id, data) => {
  try {
    const response = await fetch(`${API_URL}/jobvacancy/${id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      throw new Error(`Failed to update job vacancy: ${response.status}`);
    }
    return await response.json();
  } catch (error) {
    console.error('Error updating job vacancy:', error.message);
    throw error;
  }
};

export const fetchRecruiters = async () => {
  const response = await fetch(`${API_URL}/Recruiter`);
  if (!response.ok) {
    throw new Error('Failed to fetch recruiters');
  }
  return await response.json();
};
