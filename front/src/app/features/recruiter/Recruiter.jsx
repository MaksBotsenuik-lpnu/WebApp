import React, { useState, useEffect } from 'react';
import './Recruiter.css';
import { fetchRecruiters } from '../../services/apiService';

const Recruiters = () => {
  const [recruiters, setRecruiters] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    const getRecruiters = async () => {
      try {
        const data = await fetchRecruiters();
        setRecruiters(data);
      } catch (err) {
        setError(err.message);
      }
    };

    getRecruiters();
  }, []);

  if (error) {
    return <div className="error">Error: {error}</div>;
  }

  return (
    <div className="recruiters-container">
      <h1>Recruiters List</h1>
      <ul className="recruiters-list">
        {recruiters.map((recruiter) => (
          <li key={recruiter.id} className="recruiter-item">
            <div className="recruiter-info">
              {recruiter.firstName} {recruiter.lastName}
            </div>
            <div className="recruiter-contact">
              {recruiter.contactInfo}
            </div>
            <div className="recruiter-specialization">
              Specialization: {recruiter.specialization}
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default Recruiters;
