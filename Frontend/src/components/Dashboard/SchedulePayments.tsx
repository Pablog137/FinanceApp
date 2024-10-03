import { schedulePayments } from "../../helpers/DashboardLists";
import SchedulePaymentElement from "./SchedulePaymentElement";

export default function SchedulePayments() {
  const textColor = "text-gray-700";

  return (
    <div className="col-span-12 py-4">
      <div className="flex justify-between">
        <h1 className="text-xl font-bold">Schedule payments</h1>
        <p className="text-sm">View All</p>
      </div>
      <ul className={`${textColor} pt-4 flex flex-col gap-2`}>
        {schedulePayments.map((payment, index) => (
          <SchedulePaymentElement
            key={index}
            title={payment.title}
            nextPayment={payment.nextPayment}
            amount={payment.amount}
            icon={payment.icon}
          />
        ))}
      </ul>
    </div>
  );
}
