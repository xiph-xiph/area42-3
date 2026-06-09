import { Routes, Route } from "react-router-dom";

import HomePage from "./pages/HomePage";
import RestaurantPage from "./pages/RestaurantPage";
import SnackhoekPage from "./pages/SnackhoekPage";
import ParkonderhoudPage from "./pages/Parkonderhoudpage";
import MenuPage from "./pages/MenuPage";


function App() {
  return (
    <Routes>
      <Route path="/" element={<HomePage />} />

      <Route
        path="/restaurant"
        element={<RestaurantPage />}
      />

      <Route
        path="/snackhoek"
        element={<SnackhoekPage />}
      />

      <Route
        path="/parkonderhoud"
        element={<ParkonderhoudPage />}
      />

      <Route path="/menu"
      element={<MenuPage />}
       />
    </Routes>
  );
}

export default App;