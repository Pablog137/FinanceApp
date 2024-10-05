import { useDarkMode } from "../../context/DarkModeContext";
import BasicLineChart from "./BasicLineChart";
import Card from "./Card";
import QuickActions from "./QuickActions";
import SchedulePayments from "./SchedulePayments";

type Props = {
  colMain: string;
};

export default function Main({ colMain }: Props) {
  const { textColor } = useDarkMode();

  return (
    <>
      <div
        className={`${textColor} pt-5 md:pt-10 ${colMain} grid grid-cols-12 xl:p-20 p-10 pb-20`}
      >
        <div className="text-white p-5 lg:p-10 col-span-12 md:col-span-6 xl:col-span-4">
          <Card />
        </div>
        <div className="p-5 hidden md:col-span-6 xl:col-span-8 md:grid grid-cols-12 gap-4 items-center">
          <div className={`text-red-400 col-span-12 xl:col-span-6`}>
            <div className="flex flex-col">
              <div className="bg-[rgb(255,232,232)] border-2 p-6 rounded-2xl flex justify-between items-center">
                <div className="flex flex-col gap-2">
                  <h1 className="font-semibold">Total expenses</h1>
                  <div className="flex justify-between gap-4 items-end">
                    <div>
                      <p className="text-sm font-semibold">Last month</p>
                      <p className="text-xl lg:text-2xl font-semibold">
                        $ 1,000
                      </p>
                    </div>
                    <div>
                      <p className="text-sm font-semibold">This month</p>
                      <p className="text-xl lg:text-2xl font-semibold">
                        $ 56,000
                      </p>
                    </div>
                    <p className="font-bold text-gray-400 p-1 text-sm lg:text-md">
                      <i className="fa-solid fa-caret-up"></i>
                      <span>12%</span>
                    </p>
                  </div>
                </div>
                <div className="p-2 rounded-full bg-red-50">
                  <i className="fa-solid fa-arrow-down text-2xl"></i>
                </div>
              </div>
            </div>
          </div>

          <div className="col-span-12 xl:col-span-6">
            <div className={`text-green-500 flex flex-col`}>
              <div className="bg-[rgb(220,247,240)] border-2 p-6 rounded-2xl flex justify-between items-center">
                <div className="flex flex-col gap-2">
                  <h1 className="font-semibold">Total incomes</h1>
                  <div className="flex justify-between gap-4 items-end">
                    <div>
                      <p className="text-sm font-semibold">Last month</p>
                      <p className="text-xl lg:text-2xl font-semibold">
                        $ 1,000
                      </p>
                    </div>
                    <div>
                      <p className="text-sm font-semibold">This month</p>
                      <p className="text-xl lg:text-2xl font-semibold">
                        $ 56,000
                      </p>
                    </div>
                    <p className="font-bold text-gray-400 text-sm lg:text-md">
                      <i className="fa-solid fa-caret-down p-1"></i>
                      <span>7%</span>
                    </p>
                  </div>
                </div>
                <div className="p-2 rounded-full bg-green-50">
                  <i className="fa-solid fa-arrow-up text-2xl"></i>
                </div>
              </div>
            </div>
          </div>
        </div>
        <BasicLineChart />
        <QuickActions />
        <SchedulePayments />
      </div>
    </>
  );
}
