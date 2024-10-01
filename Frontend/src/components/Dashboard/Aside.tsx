import AsideElement from "./AsideElement";

type Props = {
  isAsideOpen: boolean;
};

export default function Aside({ isAsideOpen }: Props) {
  return (
    <aside
      id="logo-sidebar"
      className={`h-full min-h-screen pt-6 transition-transform ${
        isAsideOpen ? "" : "hidden"
      } bg-white border-r border-gray-200 sm:translate-x-0 dark:bg-gray-800 dark:border-gray-700 pt-10 flex-shrink-0 overflow-y-auto`}
      aria-label="Sidebar"
    >
      <div className="h-full flex flex-col md:px-3 xl:px-4 pb-4 bg-white dark:bg-gray-800">
        <ul className="font-medium flex flex-col items-center lg:items-start justify-start list-none flex-grow">
          <AsideElement text="Dashboard" icon="fa-solid fa-home" url="/" />
          <AsideElement
            text="Notes"
            icon="fa-solid fa-sticky-note"
            url="/notes"
          />
        </ul>
      </div>
    </aside>
  );
}
