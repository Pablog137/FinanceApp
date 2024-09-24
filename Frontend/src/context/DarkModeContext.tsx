import {
  createContext,
  useState,
  useContext,
  useEffect,
  ReactNode,
} from "react";

interface DarkModeContextProps {
  isDarkMode: boolean;
  toggleDarkMode: () => void;
  bgColor: string;
  textColor: string;
  inputStyles: string;
}
interface DarkModeProviderProps {
  children: ReactNode;
}

const DarkModeContext = createContext<DarkModeContextProps | undefined>(
  undefined
);

export const useDarkMode = (): DarkModeContextProps => {
  const context = useContext(DarkModeContext);
  if (!context) {
    throw new Error("useDarkMode must be used within a DarkModeProvider");
  }
  return context;
};

export const DarkModeProvider = ({ children }: DarkModeProviderProps) => {
  const [isDarkMode, setIsDarkMode] = useState<boolean>(() => {
    const savedMode = localStorage.getItem("darkMode");
    return savedMode ? JSON.parse(savedMode) : false;
  });

  useEffect(() => {
    localStorage.setItem("darkMode", JSON.stringify(isDarkMode));
  }, [isDarkMode]);

  const toggleDarkMode = () => {
    console.log("toggleDarkMode");
    setIsDarkMode((prevMode) => !prevMode);
  };

  const bgColor = isDarkMode ? "dark" : "white";
  const textColor = isDarkMode ? "text-white" : "text-gray-700";
  const inputStyles = isDarkMode
    ? "bg-gray-800"
    : "bg-gray-300  placeholder-gray-700";

  return (
    <DarkModeContext.Provider
      value={{ isDarkMode, toggleDarkMode, bgColor, textColor, inputStyles }}
    >
      {children}
    </DarkModeContext.Provider>
  );
};
