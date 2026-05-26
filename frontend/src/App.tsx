import { BrowserRouter, Routes, Route } from "react-router";
import Index from "./pages/Index";

const App = () => {
  return (
    <BrowserRouter>
      <Routes>
        <Route index element={<Index />} />
        <Route path="/test" element={<div>Test pagina</div>} />
      </Routes>
    </BrowserRouter>
  );
};

export default App;
