import React, { useEffect, useState } from 'react';
import { fetchJobVacancies, addJobVacancy, updateJobVacancy } from '../../services/apiService'; // Імпорт нових функцій
import VacancyForm from './VacancyForm';
import './Vacancy.css';

const Vacancy = () => {
  const [jobVacancies, setJobVacancies] = useState([]);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(true);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalVacancies, setTotalVacancies] = useState(0);
  const [isFormVisible, setIsFormVisible] = useState(false);
  const [selectedVacancy, setSelectedVacancy] = useState(null); // Для редагування

  const limit = 10;

  useEffect(() => {
    const getJobVacancies = async () => {
      setLoading(true);
      try {
        const response = await fetchJobVacancies(currentPage, limit);
        if (!response || typeof response !== 'object' || !Array.isArray(response.items)) {
          throw new Error('Invalid response structure');
        }
        setJobVacancies(response.items);
        setTotalVacancies(response.totalCount);
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    getJobVacancies();
  }, [currentPage]);

  const toggleFormVisibility = (vacancy = null) => {
    setSelectedVacancy(vacancy);
    setIsFormVisible(!isFormVisible);
  };

  const handleFormSubmit = async (formData) => {
    if (formData) {
      if (selectedVacancy) {
        // Якщо обрана вакансія - оновлення
        try {
          await updateJobVacancy(selectedVacancy.id, formData);
          console.log('Vacancy Updated:', formData);
          // Тут можна перезавантажити вакансії, щоб відобразити оновлення
        } catch (error) {
          console.error('Error updating vacancy:', error);
        }
      } else {
        // Додавання нової вакансії
        try {
          await addJobVacancy(formData);
          console.log('Vacancy Added:', formData);
          // Тут можна перезавантажити вакансії, щоб відобразити нову вакансію
        } catch (error) {
          console.error('Error adding vacancy:', error);
        }
      }
    }
    setIsFormVisible(false);
  };

  if (loading) {
    return <div className="loading">Loading...</div>;
  }

  if (error) {
    return <div className="error">Error: {error}</div>;
  }

  const totalPages = Math.ceil(totalVacancies / limit);

  const handlePageChange = (page) => {
    if (page >= 1 && page <= totalPages) {
      setCurrentPage(page);
    }
  };

  return (
    <div className="vacancies-container">
      <h1>Job Vacancies List</h1>
      <button className="add-vacancy-btn" onClick={() => toggleFormVisibility()}>
        Add New Vacancy
      </button>
      {isFormVisible && (
        <VacancyForm
          onSubmit={handleFormSubmit}
          initialData={selectedVacancy || {}}
          isEditMode={!!selectedVacancy}
        />
      )}
      <ul>
        {jobVacancies.length > 0 ? (
          jobVacancies.map((job) => (
            <li key={job.id}>
              <span>{job.jobTitle}</span>
              <span>{job.salary} {job.currency}</span>
              <button onClick={() => toggleFormVisibility(job)}>Edit</button>
            </li>
          ))
        ) : (
          <li>No job vacancies available.</li>
        )}
      </ul>
      <div className="pagination">
        <button onClick={() => handlePageChange(currentPage - 1)} disabled={currentPage === 1}>
          Previous
        </button>
        <span>Page {currentPage} of {totalPages}</span>
        <button onClick={() => handlePageChange(currentPage + 1)} disabled={currentPage === totalPages || totalPages === 0}>
          Next
        </button>
      </div>
    </div>
  );
};

export default Vacancy;
