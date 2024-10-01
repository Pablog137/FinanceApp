import { useDarkMode } from "../../context/DarkModeContext";
import AsideElement from "./AsideElement";

export default function Aside() {
  const { isDarkMode, textColor } = useDarkMode();

  const bgColor = isDarkMode ? "dark:bg-gray-800" : "bg-white";

  return (
    <aside
      id="logo-sidebar"
      className={`${bgColor} h-full min-h-screen transition-transform border-r border-gray-200 sm:translate-x-0  dark:border-gray-700 flex-shrink-0 overflow-y-auto`}
      aria-label="Sidebar"
    >
      <div className="h-full flex flex-col md:px-3 xl:px-4 pb-4 pt-6">
        <ul className="font-medium flex flex-col items-center lg:items-start justify-start list-none flex-grow">
          <AsideElement
            text="Dashboard"
            icon="fa-solid fa-home"
            textColor={textColor}
            url="/"
          />
          <AsideElement
            text="Notes"
            icon="fa-solid fa-sticky-note"
            textColor={textColor}
            url="/notes"
          />
        </ul>
      </div>
    </aside>
  );
}
