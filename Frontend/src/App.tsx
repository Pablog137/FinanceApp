import ThemedApp from "./components/ThemedApp";
import { DarkModeProvider } from "./context/DarkModeContext";
import "./styles/pages/index.css";
import { BrowserRouter } from "react-router-dom";

function App() {
  return (
    <DarkModeProvider>
      <BrowserRouter>
        <ThemedApp />
      </BrowserRouter>
    </DarkModeProvider>
  );
}
export default App;
