import { BrowserRouter, Routes, Route } from "react-router";

import Home from "./pages/Home";
import LaFamiglia from "./pages/LaFamiglia";

const App = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/la-famiglia" element={<LaFamiglia />} />
      </Routes>
    </BrowserRouter>
  );
};

export default App;
