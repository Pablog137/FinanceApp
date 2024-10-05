interface LightModeItemProps {
  isDarkMode: boolean;
  toggleDarkMode: () => void;
  textColorDark?: string;
  textColorLight?: string;
}

export default function LightModeIcon({
  isDarkMode,
  toggleDarkMode,
  textColorDark = "text-black",
  textColorLight = "text-yellow-300",
}: LightModeItemProps) {
  return (
    <>
      {isDarkMode ? (
        <i
          className={`${textColorLight} fa-solid fa-sun text-3xl cursor-pointer`}
          onClick={toggleDarkMode}
        ></i>
      ) : (
        <i
          className={`${textColorDark} fa-solid fa-moon text-3xl cursor-pointer`}
          onClick={toggleDarkMode}
        ></i>
      )}
    </>
  );
}
