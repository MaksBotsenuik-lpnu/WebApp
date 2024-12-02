import React from 'react';
import styles from './Home.module.css';

const Home = () => {
  return (
    <div className={styles.home}>
      <img src={`${process.env.PUBLIC_URL}/icons/logo.svg`} alt="Logo" className={styles.logo} />
      <h1 className={styles.title}>Welcome to the Recruitment Agency</h1>
      <p className={styles.description}>Your one-stop solution for finding the best candidates and job vacancies.</p>
    </div>
  );
};

export default Home;
