import { schedulePayments } from "../../helpers/DashboardLists";
import SchedulePaymentElement from "./SchedulePaymentElement";

export default function SchedulePayments() {
  return (
    <div className="col-span-12 pt-2">
      <div className="flex justify-between">
        <h1 className="text-xl font-bold">Schedule payments</h1>
        <p className="text-sm font-light">View All</p>
      </div>
      <ul className="pt-4 flex flex-col gap-2">
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
