// packages
import { Route, Routes } from 'react-router-dom';

// source
import Home from 'src/modules/home/pages/Home';

const HomeRoutes = () => (
  <Routes>
    <Route path="/" element={<Home />} />
  </Routes>
);

export default HomeRoutes;
