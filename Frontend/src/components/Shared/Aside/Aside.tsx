import { useDarkMode } from "../../../context/DarkModeContext";
import AsideElement from "./AsideElement";
import AsideElementList from "./AsideElementList";

export default function Aside() {
  const { isDarkMode, toggleDarkMode } = useDarkMode();

  const bgColor = isDarkMode ? "dark:bg-gray-800" : "bg-white";
  const textColor = isDarkMode ? "text-gray-200" : "text-gray-600";
  const bgHoverColor = isDarkMode ? "hover:bg-gray-700" : "hover:bg-gray-100";

  return (
    <aside
      id="logo-sidebar"
      className={`${bgColor} fixed z-10 pt-20 h-full transition-transform sm:translate-x-0 flex-shrink-0 overflow-y-auto`}
      aria-label="Sidebar"
    >
      <div className="flex flex-col h-full justify-between md:px-3 xl:px-4 pb-4 pt-6">
        <ul className="font-medium flex flex-col items-center lg:items-start justify-start list-none flex-grow gap-1">
          <AsideElementList textColor={textColor} />
        </ul>
        <ul className="font-medium flex flex-col items-center lg:items-start justify-center list-none flex-grow gap-1 pt-10">
          <div className="p-2">
            <h1 className={`${textColor} p-2 font-semibold text-sm`}>
              PREFERENCES
            </h1>
          </div>
          <AsideElement
            text="Settings"
            icon="fa-solid fa-gear"
            url="/settings"
            textColor={textColor}
            textSize="text-lg"
          />
          <li className="p-2 lg:w-full">
            <a
              onClick={toggleDarkMode}
              className={`${textColor} ${bgHoverColor} flex items-center p-2 rounded-lg group  cursor-pointer`}
            >
              {isDarkMode ? (
                <i
                  className={`${textColor} fa-solid fa-sun text-3xl cursor-pointer`}
                ></i>
              ) : (
                <i
                  className={`${textColor} fa-solid fa-moon text-3xl cursor-pointer`}
                ></i>
              )}
              <span className="flex-1 whitespace-nowrap ms-3 hidden lg:flex">
                Light
              </span>
            </a>
          </li>
        </ul>
        <ul className="font-medium flex flex-col items-center lg:items-start justify-end list-none flex-grow gap-1">
          <li className="p-2 lg:w-full">
            <a
              className={`${textColor} ${bgColor} flex items-center p-2 rounded-lg group hover:bg-gray-100 cursor-pointer`}
            >
              <i
                className={`fa-solid fa-arrow-right-from-bracket text-lg md:text-2xl transition duration-75 `}
              ></i>
              <span className="flex-1 whitespace-nowrap ms-3 hidden lg:flex">
                Sign out
              </span>
            </a>
          </li>
        </ul>
      </div>
    </aside>
  );
}
