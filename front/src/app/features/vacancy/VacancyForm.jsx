import React, { useState, useEffect } from 'react';
import './VacancyForm.css';

const VacancyForm = ({ onSubmit, initialData = {}, isEditMode = false }) => {
  const [formData, setFormData] = useState({
    jobTitle: '',
    jobDescription: '',
    vacancyStatus: '',
    salary: 0,
    currency: '',
    location: '',
    requiredSkills: [{ name: '' }]
  });

  useEffect(() => {
    if (isEditMode) {
      setFormData({
        ...initialData,
        requiredSkills: initialData.requiredSkills || [{ name: '' }]
      });
    }
  }, [initialData, isEditMode]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSkillChange = (index, e) => {
    const updatedSkills = formData.requiredSkills.map((skill, i) => 
      i === index ? { ...skill, name: e.target.value } : skill
    );
    setFormData({
      ...formData,
      requiredSkills: updatedSkills,
    });
  };

  const addSkillField = () => {
    setFormData({
      ...formData,
      requiredSkills: [...formData.requiredSkills, { name: '' }]
    });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    onSubmit(formData);
  };

  return (
    <div className="modal-overlay">
      <div className="modal-content">
        {/* Хрестик для закриття */}
        <button className="close-btn" onClick={() => onSubmit(null)}>×</button>
        
        <form className="vacancy-form" onSubmit={handleSubmit}>
          <h2>{isEditMode ? 'Edit Vacancy' : 'Add New Vacancy'}</h2>

          <label>
            Job Title:
            <input type="text" name="jobTitle" value={formData.jobTitle} onChange={handleChange} required />
          </label>

          <label>
            Job Description:
            <textarea name="jobDescription" value={formData.jobDescription} onChange={handleChange} required />
          </label>

          <label>
            Vacancy Status:
            <input type="text" name="vacancyStatus" value={formData.vacancyStatus} onChange={handleChange} required />
          </label>

          <label>
            Salary:
            <input type="number" name="salary" value={formData.salary} onChange={handleChange} required />
          </label>

          <label>
            Currency:
            <input type="text" name="currency" value={formData.currency} onChange={handleChange} required />
          </label>

          <label>
            Location:
            <input type="text" name="location" value={formData.location} onChange={handleChange} required />
          </label>

          <label>
            Required Skills:
            {formData.requiredSkills.map((skill, index) => (
              <input 
                key={index} 
                type="text" 
                value={skill.name} 
                onChange={(e) => handleSkillChange(index, e)} 
                required 
              />
            ))}
            <button type="button" onClick={addSkillField} className="add-skill-btn">Add Skill</button>
          </label>

          <div className="form-buttons">
            <button type="submit">{isEditMode ? 'Save Changes' : 'Submit'}</button>
            <button type="button" className="cancel-btn" onClick={() => onSubmit(null)}>Cancel</button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default VacancyForm;
