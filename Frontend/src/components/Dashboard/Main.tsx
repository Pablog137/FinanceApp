type Props = {
  colMain: string;
};

export default function Main({ colMain }: Props) {
  return (
    <>
      <>
        {/* <div className={colsAside}>
          <Aside isAsideOpen={isAsideOpen} type={"dashboard"} />
        </div> */}
        <div
          className={`text-white  bg-[#161922] pt-20 md:pt-40 flex flex-col items-center h-screen ${colMain}`}
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
    </>
  );
}
