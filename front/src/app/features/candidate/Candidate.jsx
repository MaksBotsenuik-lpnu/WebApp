import React, { useEffect, useState } from 'react';
import { fetchCandidates } from '../../services/apiService';
import './Candidate.css';

const Candidates = () => {
  const [candidates, setCandidates] = useState([]);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const getCandidates = async () => {
      setLoading(true);
      try {
        const data = await fetchCandidates();
        setCandidates(data);
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    getCandidates();
  }, []);

  if (loading) {
    return <div className="loading">Loading...</div>;
  }

  if (error) {
    return <div className="error">Error: {error}</div>;
  }

  return (
    <div className="candidates-container">
      <h1>Candidates List</h1>
      <div className="candidates-list">
        {candidates.map((candidate) => (
          <div key={candidate.id} className="candidate-card">
            <h2>{candidate.firstName} {candidate.lastName}</h2>
            <p><strong>Contact:</strong> {candidate.contactInfo}</p>
            <p><strong>Status:</strong> {candidate.currentStatus}</p>
            <p><strong>Skills:</strong> {candidate.skills.join(', ')}</p>
            <p><strong>Education:</strong> {candidate.education}</p>
            <details>
              <summary><strong>Work Experience</strong></summary>
              <p>{candidate.workExperience}</p>
            </details>
            <details>
              <summary><strong>Resume</strong></summary>
              <p>{candidate.resume}</p>
            </details>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Candidates;
