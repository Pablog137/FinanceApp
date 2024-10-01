import { useDarkMode } from "../../context/DarkModeContext";

type Props = {
  colMain: string;
};

export default function Main({ colMain }: Props) {
  const { isDarkMode, textColor } = useDarkMode();

  const bgColor = isDarkMode ? "bg-[#161922]" : "bg-white";

  return (
    <>
      <div
        className={`${textColor} pt-20 md:pt-40 flex flex-col items-center h-screen ${
          colMain + " " + bgColor
        }`}
      >
        <div className="flex items-center text-center">
          <h1 className="text-4xl sm:text-5xl md:text-6xl xl:text-7xl mr-4">
            Welcome to FinanceApp!!
          </h1>
          {/* <img src={logo} alt="logo" className="hidden lg:block lg:w-16 " /> */}
        </div>
        <div className="mt-14">
          <h5 className="text-xl md:text-2xl lg:text-3xl text-center">
            This is a test
          </h5>
        </div>
        <div className="border-b border-white w-full mt-20"></div>
      </div>
    </>
  );
}
