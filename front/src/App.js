import React, { Suspense, lazy } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Header from './app/components/Header';
import Spinner from './app/components/Spinner';

const Home = lazy(() => import('./app/components/Home'));
const JobVacancies = lazy(() => import('./app/features/vacancy/Vacancy'));
const Recruiters = lazy(() => import('./app/features/recruiter/Recruiter'));
const Candidates = lazy(() => import('./app/features/candidate/Candidate'));

const App = () => {
  return (
    <Router>
      <Header />
      <Suspense fallback={<Spinner />}>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/candidates" element={< Candidates />} />
          <Route path="/job-vacancies" element={< JobVacancies />} />
          <Route path="/recruiters" element={<Recruiters />} />
        </Routes>
      </Suspense>
    </Router>
  );
};

export default App;
