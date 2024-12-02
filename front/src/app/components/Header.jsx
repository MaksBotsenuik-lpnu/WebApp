import React from 'react';
import { NavLink } from 'react-router-dom'; // Use NavLink for navigation
import styles from './Header.module.css'; // Import styles

const Header = () => {
  return (
    <header className={styles.header}>
      <h1 className={styles.title}>Recruitment Agency</h1>
      <nav className={styles.nav}>
        <NavLink 
          to="/" 
          className={({ isActive }) => `${styles.link} ${isActive ? styles.active : ''}`} // Set active class conditionally
        >
          Home
        </NavLink>
        <NavLink 
          to="/candidates" 
          className={({ isActive }) => `${styles.link} ${isActive ? styles.active : ''}`} // Set active class conditionally
        >
          Candidates
        </NavLink>
        <NavLink 
          to="/job-vacancies" 
          className={({ isActive }) => `${styles.link} ${isActive ? styles.active : ''}`} // Set active class conditionally
        >
          Job Vacancies
        </NavLink>
        <NavLink 
          to="/recruiters" 
          className={({ isActive }) => `${styles.link} ${isActive ? styles.active : ''}`} // Set active class conditionally
        >
          Recruiters
        </NavLink>
      </nav>
    </header>
  );
};

export default Header;
