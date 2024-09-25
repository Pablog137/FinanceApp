interface LightModeItemProps {
  isDarkMode: boolean;
  toggleDarkMode: () => void;
}

export default function LightModeIcon({
  isDarkMode,
  toggleDarkMode,
}: LightModeItemProps) {
  return (
    <>
      {isDarkMode ? (
        <i
          className="fa-solid fa-sun text-yellow-300 text-3xl"
          onClick={toggleDarkMode}
        ></i>
      ) : (
        <i
          className="fa-solid fa-moon text-black text-3xl"
          onClick={toggleDarkMode}
        ></i>
      )}
    </>
  );
}
